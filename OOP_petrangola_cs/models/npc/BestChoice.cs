using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_petrangola_cs.models.npc
{
    class BestChoice : AbstractChoiceStrategy
    {
        public override List<ICards> ChooseCards(List<ICards> cardsList)
        {
            final List<Card> cardList = cardsList.stream()
                                      .map(Cards::getCombination)
                                      .map(Combination::getCards)
                                      .flatMap(List::stream)
                                      .collect(Collectors.toList());

            final Cards boardCards = getBoardCards(cardsList);
            final Cards playerCards = getPlayerCards(cardsList);


            final List<Card> maxCombination = getMaxCombinationListOfCards(cardList);
            final List<Card> complement = cardList
                                                .stream()
                                                .filter(card-> !maxCombination.contains(card))
                                                .collect(Collectors.toList());

            if (playerCards.getPlayer().isPresent() && maxCombination.equals(playerCards.getCombination().getCards()))
            {
                return List.of();
            }

            playerCards.getCombination().replaceCards(maxCombination, playerCards.getCombination().getCards());
            boardCards.getCombination().replaceCards(complement, boardCards.getCombination().getCards());

            return List.of(boardCards, playerCards);
        }

        private List<Card> getMaxCombinationListOfCards(List<Card> cardList)
        {
            List<List<Card>> combinations = generateAllCombinations(cardList);

            Optional<List<Card>> tris = combinations.stream().filter(CombinationChecker::isTris).findAny();

            if (tris.isPresent())
            {
                return tris.get();
            }

            Optional<List<Card>> flush = combinations.stream().filter(CombinationChecker::isFlush).findAny();

            if (flush.isPresent())
            {
                return flush.get();
            }

            Optional<List<Card>> flushWithAceLow = combinations.stream().filter(CombinationChecker::isAceLow).findAny();

            if (flushWithAceLow.isPresent())
            {
                return flushWithAceLow.get();
            }

            List<Card> list = new ArrayList<>();

            combinations
                  .stream()
                  .map(cards-> new Pair<>(cards, getMaxCombination(cards)))
                  .max(Comparator.comparingInt(Pair::getY))
                  .ifPresent(pair->list.addAll(pair.getX()));

            return list;
        }

        private List<List<Card>> generateAllCombinations(List<Card> cardList)
        {
            return Generator.combination(cardList)
                         .simple(DeckConstants.DECK_SIZE.getValue())
                         .stream()
                         .collect(Collectors.toList());
        }


        private int getMaxCombination(List<Card> cards)
        {
            AtomicInteger max = new AtomicInteger();

            cards.stream()
                  .collect(Collectors.groupingBy(Card::getSuit))
                  .entrySet()
                  .stream()
                  .map(entry-> new Pair<>(entry.getKey(), entry.getValue().stream().mapToInt(Card::getValue).sum()))
                  .max(Comparator.comparingInt(Pair::getY))
                  .ifPresent(pair->max.set(pair.getY()));

            return max.get();
        }
    }
   
}
